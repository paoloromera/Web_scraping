<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LeituraPagina.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Leitura Página</title>
    <script src='lib/js/jquery-3.3.1.slim.min.js' type='text/javascript'></script>
    <script src='lib/js/bootstrap.js' type='text/javascript'></script>
    <script src='lib/js/chart.min.js' type='text/javascript'></script>
    <script type="text/javascript" src="lib/js/moment.min.js"></script>
    <script type="text/javascript" src="lib/js/chart.min.js"></script>
    <script type="text/javascript" src="Default.js"></script>
    <link rel='stylesheet' href='lib/css/bootstrap.css' />
    <link rel='stylesheet' href='Default.css' />
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="HiddenData" runat="server" />
        <asp:HiddenField ID="HiddenLabel" runat="server" />
        <div class="input-group mb-1 col-md-12" id="header">
            <div class="input-group-prepend">
                <span class="input-group-text">WebPage</span>
            </div>
            <input type="text"
                class="form-control align-content-center " id="urlPage" value="" placeholder="WebPage" required="required" runat="server" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>
        <div class="container-carousel container-fluid">
            <div class="col-md-10 offset-1">
                <div id="carousel" class="carousel slide" data-ride="carousel">
                    <ol class="carousel-indicators" runat="server" id="buttons"></ol>
                    <div class="carousel-inner" id="itens" runat="server">
                    </div>
                    <a class="carousel-control-prev" href="#carousel" role="button" data-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="carousel-control-next" href="#carousel" role="button" data-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>
            </div>
        </div>
        <div class="col-md-8 offset-2">
            <h3 id="grid_title">Top 10 Word Counter</h3>
            <div class="table-responsive">
                <table class="table table-striped table-sm">
                    <thead>
                        <tr>
                            <th>Word</th>
                            <th>Counter</th>
                        </tr>
                    </thead>
                    <tbody id="tbody_words" runat="server">
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col-md-8 offset-2 chartCanvas">
            <canvas id="chart"></canvas>
        </div>
    </form>
</body>
</html>
