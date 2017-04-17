<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploadAsync.ascx.cs" Inherits="WebApplication1.FileUploadAsync" %>

<input id="fuFileUploadAsync" class="file-upload-async" type="file" accept="<%=Accept %>" />
<input id="hfFile" runat="server" type="hidden" />
<asp:Button ID="btnUploadFile" runat="server" OnClick="btnUploadFile_Click" hidden />

<script type="text/javascript">
    function fileUploadAsync_uploadFile(callback) {
        var fuFile = document.getElementById('fuFileUploadAsync'),
            reader = new FileReader();

        reader.onloadend = function (e, algo) {
            var file = {
                name: fuFile.files[0].name,
                data: e.target.result.substring(e.target.result.indexOf(',') + 1)
            };

            document.getElementById('<%= hfFile.ClientID %>').value = JSON.stringify(file);
            callback ? callback(file) : document.getElementById('<%= btnUploadFile.ClientID %>').click();
        };

        if (fuFile.files.length) {
            reader.readAsDataURL(fuFile.files[0]);
        }
    };
</script>
