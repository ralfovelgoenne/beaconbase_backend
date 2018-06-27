using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace dotNetCoreApi
{
    public static class DBQueries
    {
        public static string RegisterUserInRoom(string userId, string beaconId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(beaconId))
                throw new NullReferenceException("userId AND beaconId must not be whitespace or null");

            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO PersonMapping(userId, beaconId) ");
            sql.Append(string.Format("VALUES('{0}', '{1}')",userId,beaconId));

            return sql.ToString();
        }

        public static string DeregisterUserInRoom(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new NullReferenceException("userId must not be whitespace or null");

            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM PersonMapping ");
            sql.Append(string.Format("WHERE userId = '{0}'",userId));

            return sql.ToString();
        }

        public static string GetAllUsers()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT p.userId, b.roomName ");
            sql.Append("FROM personMapping AS p, beaconMapping AS b ");
            sql.Append("WHERE b.beaconId = p.beaconId ");
            sql.Append("FOR JSON PATH");

            return sql.ToString();
        }
    }
}