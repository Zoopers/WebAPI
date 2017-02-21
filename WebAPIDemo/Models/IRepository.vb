Public Interface IRepository(Of T)
    Function GetAll() As IEnumerable(Of T)
    Function GetObject(ByVal guidObject As Guid) As T
    Function Add(ByVal obj As T) As T
    Function Remove(ByVal guidObject As Guid) As Boolean
    Function Update(ByVal guidObject As Guid, ByVal obj As T) As Boolean
End Interface
