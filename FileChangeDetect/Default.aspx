<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FileChangeDetect._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="js/bootstrap-filestyle.min.js"> </script>
    <div class="jumbotron">
        <p>
            <asp:TextBox ID="PathTxt" runat="server" Height="39px"></asp:TextBox>
&nbsp;
            <asp:Button ID="AnalyzeBtn" runat="server" CssClass="btn btn-primary btn-lg" Height="39px" Text="analyze selected" Width="172px" OnClick="AnalyzeBtn_Click" />
        </p>
        <p>
            <asp:Label ID="MessageLbl" runat="server" Text="Folder added"></asp:Label>
        </p>
        <p>
            <asp:Label ID="UpdFilesLbl" runat="server" Text="Updated Files:"></asp:Label>
        </p>
        <p style="width: 936px">
            <asp:GridView ID="ModifiedFilesGrid" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="98px" Width="634px">
                <Columns>
                    <asp:BoundField DataField="FilePath" HeaderText="FilePath" />
                    <asp:BoundField DataField="ModificationDate" HeaderText="ModificationDate" />
                    <asp:BoundField DataField="Version" HeaderText="Version" />
                </Columns>
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </p>
        <p style="width: 936px">
            <asp:Label ID="DelFilesLbl" runat="server" Text="Deleted Files:"></asp:Label>
        </p>
        <p>
            <asp:GridView ID="DeletedFilesGrid" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="98px" Width="634px">
                <Columns>
                    <asp:BoundField DataField="FilePath" HeaderText="FilePath" />
                    <asp:BoundField DataField="ModificationDate" HeaderText="ModificationDate" />
                    <asp:BoundField DataField="Version" HeaderText="Version" />
                </Columns>
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </p>
        <p style="width: 936px">
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </div>

    </asp:Content>
