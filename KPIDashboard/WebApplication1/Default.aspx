<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="jumbotron" style="overflow-x: hidden; margin-top: 100px;">

        <p style="font-size: large; font-style: oblique; font-weight: bold; font-family: Calibri; height: 9px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering= true>
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <asp:Label ID="Label3" runat="server" Enabled="False" Text="Select the product"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="211px" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Height="33px">
                            <asp:ListItem Text="" Value="0"  />
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="Label2" runat="server" Enabled="False" Text="Select the version"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="213px" Height="38px">
                        </asp:DropDownList>


                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>

            
                       
  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" />
         
      
                        
   &nbsp;<div class="row">
             
      
                               <div class="col-md-4">
                    <h2 style="font-family: Calibri"><strong>Not Executed Test</strong></h2>


                    <div class="progress-bar position" data-percent="00" data-duration="1000"   data-color="#f75567,#12b321" runat="server" id="progress1">
                    </div>
                                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label4" runat="server" Enabled="False" Text=" " Font-Size="50" Font-Bold="True" BackColor="#EEEEEE" ForeColor="White"></asp:Label>
   
                </div>
                <div class="col-md-4">
                    <h2 style="font-family: Calibri"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Code Freeze</strong></h2>


                    <div class="progress-bar position" data-percent="00" data-duration="1000" data-color="#f75567,#12b321" runat="server" id="progress2">
                    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Enabled="False" Text="   " Font-Size="50" Font-Bold="True" BackColor="#EEEEEE" ForeColor="White"></asp:Label>
                </div>
                <div class="col-md-4">
                    <h2 style="font-family: Calibri"><strong>&nbsp;&nbsp;&nbsp;&nbsp; Failed Test</strong></h2>

                    <div class="progress-bar position" data-percent="00" data-duration="1000" data-color="#f75567,#12b321" runat="server" id="progress3"></div>

                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Enabled="False" Text="   " Font-Size="50" Font-Bold="True" BackColor="#EEEEEE" ForeColor="White"></asp:Label>
   


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
                    <asp:ListItem>TestNotExecuted</asp:ListItem>
                    <asp:ListItem>CodeFreeze</asp:ListItem>
                    <asp:ListItem>FailedTest</asp:ListItem>
                </asp:DropDownList>
                      
                <br />
            
                         

                 <asp:Chart ID="Chart2" runat="server" Width="943px" Height="289px" BackImageTransparentColor="238, 238, 238">
                                   <series>
                                       <asp:Series Name="Series1" ToolTip=" #VALY%" Legend="Percentage">
                                       </asp:Series>
                                   </series>

                     <Legends>
        <asp:Legend Alignment="Center" Docking="Top" IsTextAutoFit="False" Name="Percentage" LegendStyle="Row" BackColor="White" />
    </Legends>
                                   <chartareas>
                                       <asp:ChartArea Name="ChartArea1">
                                       </asp:ChartArea>
                                   </chartareas>
                               </asp:Chart>
  
   </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
  
                 

              


              
      
                               
                 

              


              
      
    </div>
    </div>
</asp:Content>
