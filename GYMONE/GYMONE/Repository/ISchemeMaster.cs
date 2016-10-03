using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GYMONE.Models;

namespace GYMONE.Repository
{
    interface ISchemeMaster
    {


        void InsertScheme(SchemeMasterDTO Scheme); // C
        IEnumerable<SchemeMasterDTO> GetSchemes(); // R
        SchemeMasterDTO GetSchemeByID(string SchemeID); // R
        void UpdateScheme(SchemeMasterDTO Scheme); //U
        void DeleteScheme(string SchemeId); //D
        //void Save();
        bool SchemeNameExists(string SchemeName);




    }
}
