using Dapper;
using SpotifyRegistrationTool.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Z.Dapper.Plus;
using System.Linq;
using SpotifyRegistrationTool.Logic;

namespace SpotifyRegistrationTool._Data
{
    public class DataAccess
    {

        public DataAccess()
        {
            DapperPlusManager.Entity<Models.AccountDb>().Table("Accounts");
            AddTable();
        }

        public void AddTable()
        {
            try
            {
                using (var connection = new SqlConnection(Common.DATA_BASE_CONNECTION_STRING))
                {
                    var query = "CREATE TABLE IF NOT EXISTS Accounts (Guid TEXT PRIMARY KEY , " +
                        "Email TEXT, " +
                        "Password TEXT, " +
                        "DisplayName TEXT, " +
                        "CardContact TEXT, " +
                        "Gender TEXT, " +
                        "BirthDate TEXT, " +
                        "Status TEXT);";
                    connection.Execute(query);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public List<Models.Account> GetListAccount(string status = "")
        {
            List<Models.AccountDb> listDbAccounts = new List<Models.AccountDb>();
            List<Models.Account> result = new List<Models.Account>();
            try
            {
                using (var connection = new SqlConnection(Common.DATA_BASE_CONNECTION_STRING))
                {

                    if (string.IsNullOrEmpty(status))
                    {
                        listDbAccounts = connection.Query<Models.AccountDb>($"Select * FROM Accounts").ToList();
                    }
                    else
                    {
                        listDbAccounts = connection.Query<Models.AccountDb>($"Select * FROM Accounts WHERE Status='{status}'").ToList();
                    }

                    listDbAccounts.ForEach(t =>
                    {
                        try
                        {
                            DateTime birthDate = Convert.ToDateTime(t.BirthDate);
                            GenderEnum gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), t.Gender, true);
                            AccountStatusEnum statusEnum = (AccountStatusEnum)Enum.Parse(typeof(AccountStatusEnum), t.Status, true);

                            result.Add(new Models.Account()
                            {
                                Guid = t.Guid,
                                Email = t.Email,
                                Address = t.Address,
                                DisplayName = t.DisplayName,
                                CardContact = t.CardContact,
                                _isRunning = false,
                                BirthDate = birthDate,
                                Gender = gender,
                                Password = t.Password,
                                Status = statusEnum
                            });
                        }
                        catch (Exception)
                        {
                        }

                    });

                }
            }
            catch (Exception ex)
            {
                return result;
            }


            return result;
        }
    }
}
