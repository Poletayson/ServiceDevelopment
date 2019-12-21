using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BL;
using Project;
namespace WebApplication3
{
    /// <summary>
    /// Сводное описание для WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet1 getMaterials()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getMaterials();
        }

        [WebMethod]
        public DataSet1 getUnits()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getUnits();
        }

        [WebMethod]
        public DataSet1 getOrganizations()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getOrganizations();
        }

        [WebMethod]
        public DataSet1 getInvoices()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getInvoices();
        }

        [WebMethod]
        public DataSet1 getPositions()
        {
            BusinessLogic BL = new BusinessLogic();
            return BL.getPositions();
        }
    }
}
