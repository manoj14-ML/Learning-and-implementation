<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CaptureCam.aspx.cs" Inherits="Test_webapplication.CaptureCam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Capture Image from Cam</title>
    <style type="text/css">
        body
        {
            font-family: Trebuchet MS;
            font-size: 10pt;
        }
    </style>
    <script  type="text/javascript" src="webcamjs/html2canvas.min.js"></script>
    <script type="text/javascript" src="webcamjs/webcam.min.js"></script>   
    <script  type="text/javascript" src="webcamjs/jquery.min.js"></script>
    <script  type="text/javascript" src="webcamjs/jquery.Jcrop.js"></script>
    <link href="webcamjs/jcrop.css" rel="stylesheet" type="text/css" />  
    <script type="text/javascript">
            function Takesnap() {

            $("[id*=impPrev]").css("display",
                  "block");
            $("[id*=srcUpload]").css("display",
                  "none");
            $("[id*=btnTakeSnap]").css("display",
                  "block");
            Webcam.set({
                width: 320,
                height: 240,
                image_format: 'jpeg',
                jpeg_quality: 90
            });
            Webcam.attach('#webcam');

        }

        function take_snapshot(btnImage) {
           
            
            Webcam.snap(function (data_uri) {
                // display results in page
                document.getElementById('webcam').innerHTML =
  '<img id="imageprev"  src="' + data_uri + '"/>';
            });

          
            $("[id*=btnTakeSnap]").css("display", "none");

            html2canvas($("#imageprev")[0]).then(function (canvas) {
                var base64 = canvas.toDataURL();

                $("[id*=hfImageData]").val(base64);
                __doPostBack(btnExport.name, "");
            });

        }
        function ShowPreview(input) {
          
            $("[id*=webcam]").css("display",
                  "none");
            $("[id*=impPrev]").css("display",
                  "none");
            $("[id*=srcUpload]").css("display",
                  "block");
            if (input.files && input.files[0]) {
                var ImageDir = new FileReader();
                ImageDir.onload = function (e) {

                    $('#srcUpload').attr('src', e.target.result);
                }
                ImageDir.readAsDataURL(input.files[0]);
            }
        }
        function Capture() {
            webcam.capture();
            return false;
        }
        $(document).ready(function () {

            $('#<%=srcUpload.ClientID%>').Jcrop({
                onSelect: SelectCropArea
            });
        });

        function SelectCropArea(c) {
            $('#<%=X.ClientID%>').val('');
            $('#<%=Y.ClientID%>').val('');
            $('#<%=W.ClientID%>').val('');
            $('#<%=H.ClientID%>').val('');
            $('#<%=X.ClientID%>').val(parseInt(c.x));
            $('#<%=Y.ClientID%>').val(parseInt(c.y));
            $('#<%=W.ClientID%>').val(parseInt(c.w));
            $('#<%=H.ClientID%>').val(parseInt(c.h));
        }  
    </script>
    <style type="text/css">
        .jcropper-holder
        {
            border: 1px black solid;
        }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
            </td>
            <td>
            </td>
            <td align="center">
            </td>
        </tr>
        <tr>
            <td>
          
                <div id="webcam">
             
                    <table>
                        <tr>
                            <td>
                               
                                <asp:Panel ID="panCrop" runat="server" Visible="false">
                                    <table  >
                                        <tr>
                                            <td>
                                                <asp:Image ID="srcUpload" runat="server"  />
                                            </td>
                                        </tr>
                                       
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="X" runat="server" />
                                                <asp:HiddenField ID="Y" runat="server" />
                                                <asp:HiddenField ID="W" runat="server" />
                                                <asp:HiddenField ID="H" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                              
                            </td>
                            <td>
                            </td>
                        </tr>
                        </table>
                </div>

                   <input type="button" id="btnTakeSnap" value="Capture  Photo" onclick="take_snapshot(this);"  style="display:none;"/>
     
            </td>
            <td>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <br />
                            <input type="button" id="btnCapture" onclick="Takesnap();" value="Take Photo"  />
                              <asp:HiddenField ID="hfImageData" runat="server" />
                             <asp:Button ID="btnExport" Text="Export to Image" runat="server" style="display:none;"  UseSubmitBehavior="false"
        OnClick="ExportToImage"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Or
                        
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Upload Photo
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload onchange="this.form.submit()" ID="fileImageUpload" runat="server" 
                                Width="211px" />
                           <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnUpload" runat="server" Text="Save" OnClick="btnUpload_Click" />
                        
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
       
         </script>
    </form>
</body>
</html>

