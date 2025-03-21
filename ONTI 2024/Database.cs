using ONTI_2024.CosmosDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONTI_2024 {
    internal class Database {
        private CosmosDBDataSet db = new CosmosDBDataSet();
        private UtilizatoriTableAdapter utilizatoriAdapter = new UtilizatoriTableAdapter();

        public bool CheckUserExists(string email) {
            return utilizatoriAdapter.GetUserByEmail(email).Rows.Count > 0;
        }
    }
}
