Public Interface ILicenseRepository
    Function GetAll() As IEnumerable(Of License)
    Function GetLicense(ByVal guidLicense As Guid) As License
    Function Add(ByVal License As License) As License
    Function Remove(ByVal guidLicenses As Guid) As Boolean
    Function Update(ByVal guidLicense As Guid, ByVal updatedLicense As License) As Boolean
End Interface
