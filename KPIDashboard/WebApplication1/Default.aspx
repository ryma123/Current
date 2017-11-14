<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
      <div class="jumbotron" style="overflow-x: hidden; margin-top:100px;">
         
        <p style="font-size: large; font-style: oblique; font-weight: bold; font-family: Calibri; height: 9px;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <asp:Label ID="Label3" runat="server" Enabled="False" Text="Select the product"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="81px">
                        </asp:DropDownList>
                        &nbsp;<asp:Label ID="Label2" runat="server" Enabled="False" Text="Select the version"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="103px">
                        </asp:DropDownList>
                        

                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" />
                        

                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
                        &nbsp;<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                       
        <div class="row">

            <div class="col-md-4">
                <h2 style="font-family: Calibri"><strong>On Time Shipment</strong></h2>
                

                    <div class="progress-bar position" data-percent="60" data-duration="1000" data-color="#a456b1,#12b321">
	
       
                </div>
            </div>
            <div class="col-md-4">
                <h2 style="font-family: Calibri"><strong>On Time Shipment</strong></h2>
                

                    <div class="progress-bar position" data-percent="60" data-duration="1000" data-color="#a456b1,#12b321">
	
       
                </div>
            </div>
            <div class="col-md-4">
                <h2 style="font-family: Calibri"><strong>On Time Shipment</strong></h2>
               
                   <div class="progress-bar position" data-percent="60" data-duration="1000" data-color="#ccc,yellow" runat="server" id="ft"></div>
  
                           <script>
		$(".progress-bar").loading();
		$('input').on('click', function () {
			 $(".progress-bar").loading();
		});
	</script>



            </div>

            <h2 style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">&nbsp;&nbsp;</h2>
            <h2 style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">&nbsp;</h2>
            <p style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">
                &nbsp;</p>
            <p style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">
                &nbsp;</p>
            <p style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">
                &nbsp;</p>
            <p style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;">
                &nbsp;</p>
            <h2 style="font-family: Calibri; margin-left: 15px; margin-bottom: 12px;"><strong>TrendLines</strong></h2>
         




        </div>






        </p>
    </div>

              
      
 
       
</asp:Content>
