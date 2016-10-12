using AjaxControlToolkit;
using ConferenceLibrary;
using DataUtilities.KTActiveDirectory;
using DataUtilities.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;


using System.Data;
using System.Collections.Specialized;
/// <summary>
/// Summary description for ConferenceWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ConferenceWebService : System.Web.Services.WebService {

    public ConferenceWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetConferenceVenues(
        string knownCategoryValues,
        string category)
    {
        int conferenceID = Conference.Instance.ID;

        DataTable table = WebDataUtility.Instance.webAppTable("sp_GetVendorsWithConferenceSpaces",
                            new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", conferenceID) });

        List<CascadingDropDownNameValue> values =
                        new List<CascadingDropDownNameValue>();

        foreach (DataRow row in table.Rows)
        {
            int vendorID = Convert.ToInt32(row["vendorID"]);
            string vendorName = Convert.ToString(row["vendorCompanyName"]);
            values.Add(new CascadingDropDownNameValue(vendorName, Convert.ToString(vendorID)));
        }

        return values.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetVenueSpaces(
        string knownCategoryValues,
        string category)
    {


        StringDictionary kv =
      CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int vendorID;
        if (!kv.ContainsKey("vendorID") ||
            !Int32.TryParse(kv["vendorID"], out vendorID))
        {
          return null;
        }

        DataTable table = WebDataUtility.Instance.webAppTable("sp_GetVendorConferenceSpaces",
                            new GenericCmdParameter[] { new GenericCmdParameter("@vendorID", vendorID) });

        List<CascadingDropDownNameValue> values =
                        new List<CascadingDropDownNameValue>();

        foreach (DataRow row in table.Rows)
        {
            string roomName = Convert.ToString(row["roomName"]);
            values.Add(new CascadingDropDownNameValue(roomName, roomName));
        }

        return values.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetSpecialEventOptions(
        string knownCategoryValues,
        string category)
    {

        StringDictionary kv =
      CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string eventType;
        if (!kv.ContainsKey("eventType"))

            return null;
        else
            eventType = kv["eventType"];

        List<CascadingDropDownNameValue> values =
                        new List<CascadingDropDownNameValue>();
        
        switch (eventType)
        {
            case "Paper":
                {
                    DataTable table = WebDataUtility.Instance.webAppTable("sp_GetSelectedPapers", 
                                    new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });

                    foreach (DataRow row in table.Rows)
                    {
                        int paperID = Convert.ToInt32(row["paperID"]);
                        string paperTitle = Convert.ToString(row["paperTitle"]);
                        values.Add(new CascadingDropDownNameValue(paperTitle, Convert.ToString(paperID)));
                    }
                    break;
                }
            case "Technical Panel":
                {
                    DataTable table = WebDataUtility.Instance.webAppTable("sp_GetTechnicalPanels",
                                    new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });


                    foreach (DataRow row in table.Rows)
                    {
                        int techPanelID = Convert.ToInt32(row["eventID"]);
                        string techpanelTitle = Convert.ToString(row["techpanelTitle"]);
                        values.Add(new CascadingDropDownNameValue(techpanelTitle, Convert.ToString(techPanelID)));
                    }
                    
                    break;
                }
            case "Conference Day":
                {
                    break;
                }
        }

        return values.ToArray();

    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetConferenceVendors
    (
        string knownCategoryValues,
        string category
    )
    {

        DataTable table = WebDataUtility.Instance.webAppTable("tbl_Vendors");
        List<CascadingDropDownNameValue> values =
                       new List<CascadingDropDownNameValue>();

        foreach (DataRow row in table.Rows)
        {
            int vendorID = Convert.ToInt32(row["vendorID"]);
            string vendorName = Convert.ToString(row["vendorCompanyName"]);
            values.Add(new CascadingDropDownNameValue(vendorName, Convert.ToString(vendorID)));
        }

        return values.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetVendorStaffMembers(
        string knownCategoryValues,
        string category)
    {


        StringDictionary kv =
      CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int vendorID;
        if (!kv.ContainsKey("vendorID") ||
            !Int32.TryParse(kv["vendorID"], out vendorID))
        {
            return null;
        }

        DataTable table = WebDataUtility.Instance.webAppTable("sp_GetVendorStaffMembers",
                            new GenericCmdParameter[] {new GenericCmdParameter("@conferenceID", Conference.Instance.ID), 
                                                        new GenericCmdParameter("@vendorID", vendorID) });

        List<CascadingDropDownNameValue> values =
                        new List<CascadingDropDownNameValue>();

        foreach (DataRow row in table.Rows)
        {
            string userEmail = DBNullable.ToString(row["userEmail"]);
            string staffName = DBNullable.ToString(row["userFullName"]);
            values.Add(new CascadingDropDownNameValue(staffName, userEmail));
        }

        return values.ToArray();
    }



    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetKTDivisions
    (
        string knownCategoryValues,
        string category
    )
    {

        DataTable table = WebDataUtility.Instance.webAppTable("sp_GetKTDivisions",
            new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) }
            );
        List<CascadingDropDownNameValue> values =
                       new List<CascadingDropDownNameValue>();

        foreach (DataRow row in table.Rows)
        {
            string division = Convert.ToString(row["userDivision"]);
            values.Add(new CascadingDropDownNameValue(division, Convert.ToString(division)));
        }

        return values.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public CascadingDropDownNameValue[] GetDivisionAttendees(
        string knownCategoryValues,
        string category)
    {


        StringDictionary kv =
      CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        string divisionText;
        if (!kv.ContainsKey("divisionText"))
        {
            return null;
        }
        else
            divisionText = kv["divisionText"];

        DataTable table = WebDataUtility.Instance.webAppTable("sp_GetDivisionAttendees",
                            new GenericCmdParameter[] {new GenericCmdParameter("@conferenceID", Conference.Instance.ID), 
                                                        new GenericCmdParameter("@divisionText", divisionText) });

        List<CascadingDropDownNameValue> values =
                        new List<CascadingDropDownNameValue>();

        foreach (DataRow row in table.Rows)
        {
            string userEmail = DBNullable.ToString(row["userEmail"]);
            string userName = DBNullable.ToString(row["userName"]);
            values.Add(new CascadingDropDownNameValue(userName, userEmail));
        }

        return values.ToArray();
    }


}
