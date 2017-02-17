Public Interface ILicenseRepository
    Function GetAll() As IEnumerable(Of License)
    Function GetLicense(ByVal guidLicense As Guid) As License
    Function Add(ByVal License As License) As License
    Function Remove(ByVal guidLicense As Guid) As Boolean
    Function Update(ByVal license As License) As Boolean
End Interface
