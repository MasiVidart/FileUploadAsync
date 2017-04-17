# FileUploadAsync
ASP.NET user control for uploading files asynchronously using update panels in web forms and without the need of triggers.

# Installation
Copy the files to your web application folder, go to Visual Studio and right click the folder you want the user control to be in → Add → Existing item → Select the .ascx file.

# Requirements
- System.Web.Extensions .dll in your application to compile properly.
- Visual Studio HTML5 compatibility. If you are using VS 2010 or older, you may need to install an extension so it let you compile.
- ScriptManager and UpdatePanel.

# Usage
### Attributes:

| Name | Description |
| - | - |
| Accept | HTML5 accept attribute for input type="file". Filters files by type in dialog |

### Events:
| Name | Description |
| - | - |
| OnFileProcessed | Acts as a callback when the user controls finishes processing the file |

### Properties:
| Name | Description |
| - | - |
| PostedFile | The file submitted as FileUploadAsyncFile type |

### Methods:
| Name | Description |
| - | - |
| ProcessFile | Process the data submitted and sets the PostedFile property. For manual use |

### Models:
| Class name | Namespace | Members |
| - | - | - |
| FileUploadAsyncFile | WebApplicationName.FileUploadAsync | Name - File name. Extension included |
| | | Data - File contents in base64 string format |
| | | Extension - Takes the extension from the file name property |
| | | GetBytes() - Get file contents in byte[] format |

### Guide:
1. Register the user control in your .aspx file with your preferred TagPrefix and TagName.
2. Include as follows:

**ASPX**:

```html
<button type="button" onclick="fileUploadAsync_uploadFile();">Upload</button>
<uc:FileUploadAsync ID="fuFile" runat="server" Accept=".jpg" OnFileProcessed="UploadFile"></uc:FileUploadAsync>
```

A simple button calling **fileUploadAsync_uploadFile** function on click is needed. Feel free to positionate and style it.

If you want to style the file upload, you can select it with CSS using **.file-upload-async**.

Accept attribute is optional.

**C#** (.aspx.cs):

```c#
protected void UploadFile(object sender, EventArgs e)
{
    // Do your stuff here with the file. e.g:
    File.WriteAllBytes(Server.MapPath($"~/{fuFile.PostedFile.Name}"), fuFile.PostedFile.GetBytes());
}
```

3. Click the button and it will raise a postback, process the file and call your event handler in your aspx code-behind.

4. Enjoy!

### Manual use Guide:
If you don´t want the button to raise the postback for you, you can provide a callback to the **fileUploadAsync_uploadFile** function and do the postback when you decide it to.

**Changes needed**:
1. Change ASPX as follows:

```html
<button type="button" onclick="fileUploadAsync_uploadFile(callback);">Upload</button>
<asp:Button ID="btnFileUpload" runat="server" OnClick="btnFileUpload_Click" hidden />
<uc:FileUploadAsync ID="fuFile" runat="server" Accept=".jpg"></uc:FileUploadAsync>
```

2. Add a javascript function to your page:

```js
function callback(file) {
    // Do some stuff before sending to the server
    console.log(file.name + ' processed!');

    // Click the button triggering the submit so it raises the postback
    document.getElementById('btnFileUpload').click();
};
```

The file received is an object with properties "name" and "data". The data is the base64 string representation of the file contents.

3. Make the event handler of the button click as follows:

```c#
protected void btnFileUpload_Click(object sender, EventArgs e)
{
    fuFile.ProcessFile(); // Tell the user control to process the data received and set the PostedFile Property
    
    // Same code as normal
}
```
