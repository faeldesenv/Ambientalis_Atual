Call RunIt()
Sub RunIt()

Dim RequestObj
Dim URL
Set RequestObj = CreateObject("Microsoft.XMLHTTP")

'Request URL...
URL = "http://sustentar.inf.br/sistema/Notificacao/AtualizarEventos.aspx?idConfig=1&de=1000&ate=1200"

'Open request and pass the URL
RequestObj.open "POST", URL , false

'Send Request
RequestObj.Send

'cleanup
Set RequestObj = Nothing
End Sub