using ConferenceLibrary;
using DataUtilities.SQLServer;
using DataUtilities.KTActiveDirectory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


public partial class EventManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable conferences = WebDataUtility.Instance.webAppTable("tbl_Conference");
            loadPanels();
            load_ManageAgendaFeatures();
        }
        else
        {
        }
    }

    private void load_ManageAgendaFeatures()
    {
        load_EventsTree();
        load_EventTypes();
        load_EventTimeCombos();
    }

    private void load_EventsTree()
    {
        tv_Events.Nodes.Clear();

        DataTable table = WebDataUtility.Instance.webAppTable("sp_admin_GetAllConferenceEvents",
            new GenericCmdParameter[] { new GenericCmdParameter("@conferenceID", Conference.Instance.ID) });
        ddl_ParentEvent.DataSource = table;
        ddl_ParentEvent.DataTextField = "EventText";
        ddl_ParentEvent.DataValueField = "EventID";
        ddl_ParentEvent.DataBind();

        ddl_ParentEvent.Items.Insert(0, new ListItem("Select Parent Event (if app.):", "0"));

        TreeNodeBinding tnb = new TreeNodeBinding();
        tnb.DataMember = "System.Data.DataRowView";
        tnb.TextField = "eventText";
        tnb.ValueField = "eventID";
        tnb.PopulateOnDemand = false;
        tnb.SelectAction = TreeNodeSelectAction.SelectExpand;
        tv_Events.DataBindings.Add(tnb);
        tv_Events.DataSource = new DataUtilities.HierarchicalDataSet(table.DefaultView, "eventID", "parentEventID");
        tv_Events.DataBind();
    }

    private void load_EventTypes()
    {
        DataTable tbl = WebDataUtility.Instance.webAppTable("tbl_EventTypes");
        ddl_EventTypes.DataSource = tbl;
        ddl_EventTypes.DataTextField = "eventType";
        ddl_EventTypes.DataValueField = "eventType";
        ddl_EventTypes.DataBind();
    }

    private void load_EventTimeCombos()
    {

        for (int hour = 1; hour <= 12; hour++)
        {
            ddl_EventStart_Hour.Items.Add(new ListItem(Convert.ToString(hour), Convert.ToString(hour)));
            ddl_EventStop_Hour.Items.Add(new ListItem(Convert.ToString(hour), Convert.ToString(hour)));
        }
        for (int min = 0; min <= 59; min++)
        {
            ddl_EventStart_Minute.Items.Add(new ListItem(min.ToString("D2"), min.ToString("D2")));
            ddl_EventStop_Minute.Items.Add(new ListItem(min.ToString("D2"), min.ToString("D2")));
        }
        ddl_EventStart_Hour.Items[0].Selected = true;
        ddl_EventStop_Hour.Items[0].Selected = true;
        ddl_EventStart_Minute.Items[0].Selected = true;
        ddl_EventStop_Minute.Items[0].Selected = true;
    }

    protected void tv_Events_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        DataRowView drv = (DataRowView)e.Node.DataItem;
        DateTime start = Convert.ToDateTime(drv["eventStart"]);

        e.Node.Text = (e.Node.Parent != null ? "[" + start.ToShortTimeString() + "] " : string.Empty)
                            + Convert.ToString(drv["eventText"]);
        e.Node.Value = Convert.ToString(drv["eventID"]);

    }

    private TreeNode createNewEventNode(ConferenceEvent confEvent)
    {
        TreeNode node = new TreeNode("[" + confEvent.Start.ToShortTimeString() + "] " + confEvent.Title);
        node.Value = Convert.ToString(confEvent.ID);
        return node;
    }

    private void insertTreeNode(ConferenceEvent confEvent, string oldPath)
    {

        if (confEvent.ParentID == 0) //root node - do not delete - only update text
        {
            tv_Events.Nodes[0].Text = confEvent.Title;
        }
        else
        {
            String parentPath = confEvent.GetParentPath().Replace("/" + confEvent.ID, string.Empty);
            TreeNode parent = tv_Events.FindNode(parentPath);
            TreeNode oldNode = tv_Events.FindNode(oldPath);
            tv_Events.Nodes.Remove(oldNode);
            int i = 0;

            TreeNode newNode = new TreeNode("[" + confEvent.Start.ToShortTimeString() + "] "
                            + confEvent.Title);
            newNode.Value = DBNullable.ToString(confEvent.ID);

            foreach (TreeNode node in parent.ChildNodes)
            {
                i = parent.ChildNodes.IndexOf(node);

                ConferenceEvent sibling = new ConferenceEvent(DBNullable.ToInt(node.Value));
                if (sibling.Start == confEvent.Start) // evalutate duration
                {
                    if (sibling.Stop.Subtract(sibling.Start) < confEvent.Stop.Subtract(confEvent.Stop))
                    {
                        parent.ChildNodes.AddAt(i, newNode);
                        break;
                    }

                }
                else if (sibling.Start > confEvent.Start)
                {
                    parent.ChildNodes.AddAt(i, newNode);
                    break;
                }
            }

        }
    }


    protected void tv_Events_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode node = tv_Events.SelectedNode;
        int eventID;
        if (int.TryParse(node.Value, out eventID))
        {

            ConferenceEvent confEvent = new ConferenceEvent(eventID);
            //populate Event Modal Popup

            ddl_EventTypes.SelectedIndex = findListIndex(ddl_EventTypes.Items, confEvent.Type);
            cascex_Venues.SelectedValue = Convert.ToString(confEvent.VenueID);
            cascex_VenueSpaces.SelectedValue = confEvent.Location;
            tb_EventTitle.Text = confEvent.Title;

            tb_EventStart_Date.Text = confEvent.Start.ToShortDateString();
            ddl_EventStart_Hour.SelectedIndex = findListIndex(ddl_EventStart_Hour.Items,
                Convert.ToString(HelperFunctions.Common.Convert24To12Hours(confEvent.Start.Hour)));
            ddl_EventStart_Minute.SelectedIndex = findListIndex(ddl_EventStart_Minute.Items, Convert.ToString(confEvent.Start.Minute));
            ddl_Event_Start_Meridian.SelectedIndex = findListIndex(ddl_Event_Start_Meridian.Items, confEvent.Start.Hour >= 12 ? "PM" : "AM");

            tb_EventStop_Date.Text = confEvent.Stop.ToShortDateString();
            ddl_EventStop_Hour.SelectedIndex = findListIndex(ddl_EventStop_Hour.Items,
                Convert.ToString(HelperFunctions.Common.Convert24To12Hours(confEvent.Stop.Hour)));
            ddl_EventStop_Minute.SelectedIndex = findListIndex(ddl_EventStop_Minute.Items, Convert.ToString(confEvent.Stop.Minute));
            ddl_Event_Stop_Meridian.SelectedIndex = findListIndex(ddl_Event_Stop_Meridian.Items, confEvent.Stop.Hour >= 12 ? "PM" : "AM");

            ddl_ParentEvent.SelectedIndex = findListIndex(ddl_ParentEvent.Items, Convert.ToString(confEvent.ParentID));
            cb_PublicEvent.Checked = confEvent.IsPublic;

            Session["activeEvent"] = confEvent;
            mdl_EventDetailsModal.Show();
        }
    }

    protected void bn_EventDetailsModalSave_Click(object sender, EventArgs e)
    {
        showUtilityModalPopup("updateEvent", "Save Changes?", "This will modify the database. Continue?", string.Empty, "OK", false);
    }
    //bn_EventDetailsModalSave_Click  bn_EventDetailsModalCancel_Click
    protected void bn_EventDetailsModalDelete_Click(object sender, EventArgs e)
    {
        ConferenceEvent confEvent = (ConferenceEvent)Session["activeEvent"];
        confEvent.Delete();
        tv_Events.Nodes.Remove(tv_Events.SelectedNode);
    }

    protected void bn_EventDetailsModalEdit_Click(object sender, EventArgs e)
    {
        setUpEventDetailsModal(true);
        //up_ManageEvent.Update();
        up_ManageEventControls.Update();
    }

    protected void bn_CreateNewEvent(object sender, EventArgs e)
    {
        setUpEventDetailsModal(true);
        Session["activeEvent"] = new ConferenceEvent(Conference.Instance.Start, Conference.Instance.Stop, string.Empty, string.Empty, string.Empty, 0, string.Empty, 1, false);
        mdl_EventDetailsModal.Show();
    }


    protected void bn_EventDetailsModalCancel_Click(object sender, EventArgs e)
    {
        setUpEventDetailsModal(false);
        mdl_EventDetailsModal.Hide();
        Session["activeEvent"] = null;
        tv_Events.SelectedNode.Selected = false;
    }

    private void setUpEventDetailsModal(bool enableForEdit)
    {
        ddl_EventTypes.Enabled = enableForEdit;
        ddl_Venues.Enabled = enableForEdit;
        ddl_Rooms.Enabled = enableForEdit;
        tb_EventTitle.Enabled = enableForEdit;
        tb_EventStart_Date.Enabled = enableForEdit;
        tb_EventStop_Date.Enabled = enableForEdit;
        ddl_EventStart_Hour.Enabled = enableForEdit;
        ddl_EventStart_Minute.Enabled = enableForEdit;
        ddl_Event_Start_Meridian.Enabled = enableForEdit;
        ddl_EventStop_Hour.Enabled = enableForEdit;
        ddl_EventStop_Minute.Enabled = enableForEdit;
        ddl_Event_Stop_Meridian.Enabled = enableForEdit;
        ddl_ParentEvent.Enabled = enableForEdit;
        bn_EventDetailsEdit.Enabled = !enableForEdit;
        bn_EventDetailsModalSave.Enabled = enableForEdit;
        cb_PublicEvent.Enabled = enableForEdit;
    }

    protected void cbo_ConferenceSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        AjaxControlToolkit.ComboBox me = (AjaxControlToolkit.ComboBox)sender;
        Application["conferenceID"] = me.SelectedValue;
    }

    protected void lb_ManageAgenda_Click(object sender, EventArgs e)
    {
        togglePanels(pnl_ManageAgenda);
    }

    private void togglePanels(Panel target)
    {
        List<Panel> panels = null;
        if (Session["panelsList"] != null)
            panels = (List<Panel>)Session["panelsList"];
        else
            return;

        foreach (Panel pnl in panels)
        {
            if (target.ClientID == pnl.ClientID)
                target.Visible = true;
            else
                target.Visible = false;
        }
    }

    private void loadPanels()
    {
        List<Panel> panels = new List<Panel>();
        panels.Add(this.pnl_ManageAgenda);

        Session["panelsList"] = panels;
    }

    private int findListIndex(ListItemCollection list, string value)
    {
        int i = 0;
        bool selected = false;
        foreach (ListItem item in list)
        {
            if (item.Value == value)
            {
                selected = true;
                break;
            }
            else
                i++;
        }
        if (!selected)
            i = 0;
        return i;
    }

    private void showUtilityModalPopup(string purpose, string heading, string msg, string initialInputText, string OKButtonText, bool needTextBox)
    {
        hdn_UtilityModalPurpose.Value = purpose;
        lbl_UtilityModalHeader.Text = heading;
        lbl_UtilityModalMessage.Text = msg;

        tb_UtilityModalEntry.Visible = needTextBox;
        if (needTextBox)
        {
            tb_UtilityModalEntry.Text = initialInputText;
            tb_UtilityModalEntry.Focus();
        }
        bn_UtilityModalSave.Text = OKButtonText;

        bn_UtilityModaClose.CssClass = (purpose == "validation" ? "hidden" : String.Empty);
        lbl_UtilityModalMessage.CssClass = (purpose == "validation" ? "validationMsg" : String.Empty);

        // up_UtilityModal.Update();
        mdl_UtilityModal.Show();

    }

    protected void tb_UtilityModalEntry_Validate()
    {
        if (hdn_UtilityModalPurpose.Value == "nonvite" || hdn_UtilityModalPurpose.Value == "adminvite")
        {
            try
            {
                KTConferenceUser user = new KTConferenceUser(tb_UtilityModalEntry.Text);
                Session["rsvpUser"] = null;
                if (user.IsInvitee)
                    Session["rsvpUser"] = user;

                val_TextEntry.IsValid = (user.IsInvitee);
            }
            catch (Exception e)
            {
                Session["rsvpUser"] = null;
                val_TextEntry.IsValid = false;
            }
        }
        else
        {
            val_TextEntry.IsValid = true;
        }
    }

    private void updateEventData(ref ConferenceEvent confEvent)
    {
        if (!confEvent.Type.Equals(ddl_EventTypes.SelectedValue))
            confEvent.Type = ddl_EventTypes.SelectedValue;
        if (!confEvent.Title.Equals(tb_EventTitle.Text))
            confEvent.Title = tb_EventTitle.Text;
        if (!confEvent.Location.Equals(ddl_Rooms.SelectedValue))
            confEvent.Location = ddl_Rooms.SelectedValue;
        if (!confEvent.ParentID.Equals(ddl_ParentEvent.SelectedValue))
            confEvent.ParentID = DBNullable.ToInt(ddl_ParentEvent.SelectedValue);

        DateTime start = Convert.ToDateTime(tb_EventStart_Date.Text);
        start = start.AddHours(HelperFunctions.Common.Convert12To24Hours(
                            DBNullable.ToInt(ddl_EventStart_Hour.SelectedValue),
                            ddl_Event_Start_Meridian.SelectedValue));
        start = start.AddMinutes(DBNullable.ToInt(ddl_EventStart_Minute.SelectedValue));

        DateTime stop = Convert.ToDateTime(tb_EventStop_Date.Text);
        stop = stop.AddHours(HelperFunctions.Common.Convert12To24Hours(
                            DBNullable.ToInt(ddl_EventStop_Hour.SelectedValue),
                            ddl_Event_Stop_Meridian.SelectedValue));
        stop = stop.AddMinutes(DBNullable.ToInt(ddl_EventStop_Minute.SelectedValue));

        if (!confEvent.Start.Equals(start))
            confEvent.Start = start;
        if (!confEvent.Stop.Equals(stop))
            confEvent.Stop = stop;
        confEvent.IsPublic = cb_PublicEvent.Checked;
    }

    protected void bn_UtilityModalSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        switch (hdn_UtilityModalPurpose.Value)
        {
            case ("updateEvent"):
                {
                    ConferenceEvent confEvent = (ConferenceEvent)Session["activeEvent"];


                    updateEventData(ref confEvent);
                    confEvent.Upload();
                    mdl_EventDetailsModal.Hide();

                    load_EventsTree();
                    setUpEventDetailsModal(false);
                    break;
                }
            case ("newEvent"):
                {
                    ConferenceEvent confEvent = (ConferenceEvent)Session["activeEvent"];
                    updateEventData(ref confEvent);
                    confEvent.Upload();
                    mdl_EventDetailsModal.Hide();

                    TreeNode newNode = createNewEventNode(confEvent);

                    load_EventsTree();
                    up_tv_Event.Update();
                    setUpEventDetailsModal(false);

                    break;
                }
            case ("deletEvent"):
                {
                    ConferenceEvent confEvent = (ConferenceEvent)Session["activeEvent"];

                    confEvent.Delete();
                    mdl_EventDetailsModal.Hide();

                    tv_Events.Nodes.Remove(tv_Events.SelectedNode);
                    up_tv_Event.Update();
                    setUpEventDetailsModal(false);

                    break;
                }
            case ("cancelSaveEvent"):
                {

                    break;
                }
            default:
                {
                    break;
                }
        }
        if (val_TextEntry.IsValid)
        {
            hdn_UtilityModalPurpose.Value = string.Empty;
            lbl_UtilityModalMessage.Text = string.Empty;
            tb_UtilityModalEntry.Text = string.Empty;
            lbl_Error.Text = string.Empty;
            lbl_Error.Visible = false;
            mdl_UtilityModal.Hide();
        }
        else
        {
            lbl_Error.Visible = true;
            up_UtilityModal.Update();
            mdl_UtilityModal.Show();
        }
        if (tv_Events.SelectedNode != null)
            tv_Events.SelectedNode.Selected = false;
    }


    protected void bn_UtilityModalCancel_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        switch (hdn_UtilityModalPurpose.Value)
        {
            case ("updateEvent"):
                {
                    break;
                }
            case ("cancelSaveEvent"):
                {
                    break;
                }

        }

        hdn_UtilityModalPurpose.Value = string.Empty;
        lbl_UtilityModalMessage.Text = string.Empty;
        tb_UtilityModalEntry.Text = string.Empty;
        lbl_Error.Text = string.Empty;
        lbl_Error.Visible = false;
        mdl_UtilityModal.Hide();
        tv_Events.SelectedNode.Selected = false;

    }


}