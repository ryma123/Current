<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="jumbotron" style="overflow-x: hidden; margin-top: 100px;">

        <p style="font-size: large; font-style: oblique; font-weight: bold; font-family: Calibri; height: 9px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering= true>
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <asp:Label ID="Label3" runat="server" Enabled="False" Text="Select the product"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="141px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                            <asp:ListItem Text="" Value="0"  />
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="Label2" runat="server" Enabled="False" Text="Select the version"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="142px">
                        </asp:DropDownList>


                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>

            
                       
  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" />
         
      
                        
   &nbsp;<div class="row">
             
      
                               <div class="col-md-4">
                    <h2 style="font-family: Calibri"><strong>Not Executed Test</strong></h2>


                    <div class="progress-bar position" data-percent="00" data-duration="1000" data-color="#f75567,#12b321" runat="server" id="progress1">
                    </div>
                </div>
                <div class="col-md-4">
                    <h2 style="font-family: Calibri"><strong>Code Freeze</strong></h2>


                    <div class="progress-bar position" data-percent="00" data-duration="1000" data-color="#f75567,#12b321" runat="server" id="progress2">
                    </div>
                </div>
                <div class="col-md-4">
                    <h2 style="font-family: Calibri"><strong>Failed Test</strong></h2>

                    <div class="progress-bar position" data-percent="00" data-duration="1000" data-color="#f75567,#12b321" runat="server" id="progress3"></div>




                    <script>
                        $(".progress-bar").loading();
                        $('input').on('click', function () {
                            $(".progress-bar").loading();
                        });
                    </script>



                </div>

                 
       <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" autopostback="false">
                <ContentTemplate>
                    <fieldset>
             <h2 style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">&nbsp;&nbsp;</h2>
                <h2 style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">&nbsp;</h2>
               
 
                        <h2 style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;"><strong>TrendLines</strong></h2>
           
                <asp:DropDownList ID="kpiSelectorDropDown" runat="server" OnSelectedIndexChanged="kpiSelectorDropDown_SelectedIndexChanged"
                    AutoPostBack="True" Width="239px">
                     <asp:ListItem>Select KPI</asp:ListItem>
                    <asp:ListItem>OnTimeShipment</asp:ListItem>
                    <asp:ListItem>CodeFreeze</asp:ListItem>
                    <asp:ListItem>TestCoverage</asp:ListItem>
                </asp:DropDownList>
                      
                <br />
            
                          <cc1:LineChart ID="LineChart1" runat="server" ChartHeight="300" ChartWidth="1000" ChartTitleColor="#0E426C" Visible="False"
                            CategoryAxisLineColor="#eeeeee" ValueAxisLineColor="#eeeeee" LineColor="#1ac92f"
                            BaseLineColor="#000000" Style="margin-left: 0px; margin-right: 0px; margin-top: 0px" Font-Names="version-percentage" ForeColor="#EEEEEE" BackColor="#EEEEEE" Width="1005px" TooltipBackgroundColor="#ffffff" TooltipBorderColor="#ffffff" TooltipFontColor="#000000"  Font-Size="8pt" DisplayValues="False" AreaDataLabel="%" CategoriesAxis="" ChartTitle="" Theme="" ValueAxisLines="0"  >
                         </cc1:LineChart>

                 
   </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
  
                 

              


              
      
    </div>
    </div>
</asp:Content>
