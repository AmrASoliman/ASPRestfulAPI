Imports System.Net
Imports System.Web.Http
Imports Newtonsoft.Json

Namespace Controllers
    Public Class PersonController
        Inherits ApiController

        ' GET: api/Person
        Public Function GetValues() As String

            Dim DL As New PersonDL
            Dim ListofPersons As List(Of Person) = DL.GetPersons
            Dim response As String = JsonConvert.SerializeObject(ListofPersons, Formatting.None)
            MsgBox(response)
            Return response
        End Function

        ' GET: api/Person/5
        Public Function GetValue(ByVal id As Integer) As String
            Dim DL As New PersonDL
            Dim Person As Person = DL.FindPerson(id)
            Return JsonConvert.SerializeObject(Person, Formatting.None)

        End Function

        ' POST: api/Person
        Public Sub PostValue(<FromBody()> ByVal value As String)

            Dim myperson As Person = JsonConvert.DeserializeObject(Of Person)(value)

            Dim DL As New PersonDL
            DL.CreatePerson(myperson)


        End Sub

        ' PUT: api/Person/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)
            Dim DL As New PersonDL
            Dim Person As Person = DL.FindPerson(id)

            Dim myperson As Person = JsonConvert.DeserializeObject(Of Person)(value)
            If Person.Name <> myperson.Name Then
                Person.Name = myperson.Name
            End If

            If Person.BirthDate <> myperson.BirthDate Then
                Person.BirthDate = myperson.BirthDate
            End If

            DL.UpdatePerson(Person)


        End Sub

        ' DELETE: api/Person/5
        Public Sub DeleteValue(ByVal id As Integer)
            Dim DL As New PersonDL
            DL.DeletePerson(id)
        End Sub
    End Class
End Namespace