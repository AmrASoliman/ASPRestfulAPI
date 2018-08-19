Imports MongoDB.Bson
Imports MongoDB.Driver
Imports MongoDB.Driver.Core
Imports RestfulApiApplication

Public Class PersonDL
    Dim server As MongoServer
    Dim client As MongoClient
    Dim db As MongoDatabase



    Public Sub New()
        client = New MongoClient("mongodb://localhost/")
        db = client.GetServer().GetDatabase("RestfulApiDb")

    End Sub

    Public Sub CreatePerson(person As Person)
        Dim collection As MongoCollection = db.GetCollection(Of BsonDocument)("Person")

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'CREATE
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim _person As BsonDocument = New BsonDocument
        With _person
            .Add("PersonID", person.ID)
            .Add("Name", person.Name)
            .Add("BirthDate", person.BirthDate)

        End With
        collection.Insert(_person)

    End Sub

    Public Function GetPersons() As List(Of Person)
        Dim collection As MongoCollection(Of BsonDocument) = db.GetCollection(Of BsonDocument)("Person")

        Dim res As New List(Of Person)
        For Each Item As BsonDocument In collection.FindAll()
            Dim Person As New Person

            Dim PersonID As BsonElement = Item.GetElement("PersonID")
            Person.ID = PersonID.Value

            Dim name As BsonElement = Item.GetElement("Name")
            Person.Name = name.Value

            Dim BirthDate As BsonElement = Item.GetElement("BirthDate")
            Person.BirthDate = BirthDate.Value

            res.Add(Person)
        Next

        Return res

    End Function

    Public Function FindPerson(id As Integer) As Person

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'READ
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Dim collection As MongoCollection(Of BsonDocument) = db.GetCollection(Of BsonDocument)("Person")

        Dim Res As New List(Of Person)
        Dim query = New QueryDocument("PersonID", id)

        Dim item As BsonDocument = collection.FindOne(query)

        Dim Person As New Person

            Dim PersonID As BsonElement = item.GetElement("PersonID")
            Person.ID = PersonID.Value

            Dim name As BsonElement = item.GetElement("Name")
            Person.Name = name.Value

            Dim BirthDate As BsonElement = item.GetElement("BirthDate")
            Person.BirthDate = BirthDate.Value

        Return Person

    End Function

    Public Sub UpdatePerson(person As Person)
        Dim collection As MongoCollection(Of BsonDocument) = db.GetCollection(Of BsonDocument)("Person")

        Dim qs = New QueryDocument("PersonID", person.ID)
        Dim found As BsonDocument = Collection.Find(qs).First
        ' Console.WriteLine(found.GetElement("location").Value.ToString)
        found.Set("Name", person.Name)
        found.Set("BirthDate", person.BirthDate)
        collection.Save(found)
    End Sub

    Public Sub DeletePerson(ID As Integer)
        Dim collection As MongoCollection(Of BsonDocument) = db.GetCollection(Of BsonDocument)("Person")
        Dim q = New QueryDocument("PersonID", ID)
        collection.Remove(q)

    End Sub



End Class
