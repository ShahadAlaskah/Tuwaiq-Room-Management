// using Ardalis.GuardClauses;
// using API.Domain.Events;
// using API.Domain.Helpers;
// using API.Domain.Identity;
// using API.Domain.Identities;
// using API.Shared.Domain;
// using API.Shared.Exceptions;
// using API.Shared.Ids;
//
// namespace API.Domain.Domains
// {
//     public sealed class Entity : BaseEntity
//     {
//         public EntityId Id { get; set; }
//
//
//         public Entity(OrganizationGroupCityId organizationGroupCityId, string nameEn, string nameAr, string? code = null, double? latitude = null, double? longitude = null, string? address = null, string? tele = null, string? teleExt = null, string? fax = null, string? faxExt = null, string? email = null)
//         {
//
//             OrganizationGroupCityId = Guard.Against.ValidId(organizationGroupCityId, nameof(OrganizationGroupCityId));
//             NameEn = Guard.Against.NullOrWhiteSpace(nameEn, nameof(NameEn));
//             NameAr = Guard.Against.NullOrWhiteSpace(nameAr, nameof(NameAr));
//             if (!string.IsNullOrWhiteSpace(code))
//                 Code = Guard.Against.NullOrWhiteSpace(code, nameof(Code));
//
//
//             if (latitude != null)
//                 Latitude = Guard.Against.NegativeOrZero(latitude.Value, nameof(Latitude));
//
//
//             if (longitude != null)
//                 Longitude = Guard.Against.NegativeOrZero(longitude.Value, nameof(Longitude));
//
//             if (!string.IsNullOrWhiteSpace(address))
//                 Address = Guard.Against.NullOrWhiteSpace(address, nameof(Address));
//
//             if (!string.IsNullOrWhiteSpace(tele))
//                 Tele = Guard.Against.NullOrWhiteSpace(tele, nameof(Tele));
//
//             if (!string.IsNullOrWhiteSpace(teleExt))
//                 TeleExt = Guard.Against.NullOrWhiteSpace(teleExt, nameof(TeleExt));
//
//             if (!string.IsNullOrWhiteSpace(fax))
//                 Fax = Guard.Against.NullOrWhiteSpace(fax, nameof(Fax));
//
//             if (!string.IsNullOrWhiteSpace(faxExt))
//                 FaxExt = Guard.Against.NullOrWhiteSpace(faxExt, nameof(FaxExt));
//
//             if (!string.IsNullOrWhiteSpace(email))
//                 Email = Guard.Against.NullOrWhiteSpace(email, nameof(Email));
//
//         }
//
//         public ReferenceEntityTypeId? ReferenceEntityTypeId { get; set; }
//         public ReferenceEntityType? ReferenceEntityType { get; set; }
//
//         public OrganizationGroupCityId OrganizationGroupCityId { get; set; }
//         public OrganizationGroupCity? OrganizationGroupCity { get; set; }
//
//         public string Name => Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft ? NameAr : NameEn;
//         public string NameEn { get; set; }
//         public string NameAr { get; set; }
//
//         public string? Code { get; set; }
//         public double? Latitude { get; set; }
//         public double? Longitude { get; set; }
//         public string? Address { get; set; }
//         public string? Tele { get; set; }
//         public string? TeleExt { get; set; }
//         public string? Fax { get; set; }
//         public string? FaxExt { get; set; }
//         public string? Email { get; set; }
//         public DateTime StartWorkingDate { get; set; } = DateTime.Now;
//         public DateTime? CloseDate { get; private set; }
//         public bool IsActive { get; private set; } = true;
//
//         public UserId? UserIdentityId { get; set; }
//         public UserIdentity? UserIdentity { get; private set; } = null!;
//
//         //public ICollection<Office> Offices { get; set; } = new HashSet<Office>();
//
//
//         private readonly List<UserIdentityUserRoleContainer> _userIdentityUserRoleContainers = new();
//         public IReadOnlyCollection<UserIdentityUserRoleContainer> UserIdentityUserRoleContainers => _userIdentityUserRoleContainers.ToList();
//
//
//         private readonly List<Office> _offices = new();
//         public IReadOnlyCollection<Office> Offices => _offices.ToList();
//
//
//         public Office AddOffice(ReferenceFloorId floorId, string nameEn, string nameAr)
//         {
//             var office = new Office(Id, Guard.Against.ValidId(floorId, nameof(ReferenceFloorId))
//                 , Guard.Against.NullOrWhiteSpace(nameEn, nameof(NameEn))
//                 , Guard.Against.NullOrWhiteSpace(nameAr, nameof(NameAr))
//             );
//             _offices.Add(office);
//
//             return office;
//         }
//
//         public void RemoveOffice(OfficeId officeId)
//         {
//             var id = Guard.Against.ValidId(officeId, nameof(OfficeId));
//
//             var office = _offices.Find(s => s.Id == id);
//
//             if (office == null) throw new FormsNotFoundException("Office");
//
//             _offices.Remove(office);
//
//             if (office.UserId != null)
//                 PublishEvent(new DeactivateUsersFormOfficeEvent(id, office.UserId.Value));
//         }
//
//
//         public void UpdateOffice(OfficeId officeId, string nameEn, string nameAr)
//         {
//             var item = _offices.Find(s => s.Id == officeId);
//
//             if (item == null) throw new FormsNotFoundException("Office");
//
//             item.NameEn = Guard.Against.NullOrWhiteSpace(nameEn, nameof(City.NameEn));
//             item.NameAr = Guard.Against.NullOrWhiteSpace(nameAr, nameof(City.NameAr));
//             
//             PublishEvent(new UpdateOfficeEvent(officeId, item));
//         }
//
//         public void SetCloseDate(DateTime closeDate)
//         {
//             PublishEvent(new DeactivateUsersFormEntityEvent(Id));
//             IsActive = false;
//             CloseDate = closeDate;
//             if (UserIdentityId != null)
//                 ClearManager();
//         }
//
//         public void Reopen()
//         {
//             IsActive = true;
//             CloseDate = null;
//         }
//
//         public void SetActive(bool isActive=true)
//         {
//             if (IsActive && !isActive) PublishEvent(new DeactivateUsersFormEntityEvent(Id));
//             IsActive = isActive;
//         }
//
//         public void ToggleActive()
//         {
//             if (IsActive) PublishEvent(new DeactivateUsersFormEntityEvent(Id));
//             IsActive = !IsActive;
//         }
//
//         public void SetManager(UserId userId)
//         {
//             var newId = Guard.Against.ValidId(userId, nameof(UserIdentityId));
//
//             if (userId == UserIdentityId) throw new FormsInvalidException("SameManager");
//
//             PublishEvent(new ValidateIsUserManagerEvent(userId));
//
//             var oldUserId = UserIdentityId;
//             if (oldUserId != null)
//             {
//                 PublishIdentity(new RemoveManagerIdentity(oldUserId.Value));
//             }
//
//             UserIdentityId = newId;
//             PublishIdentity(new AssignManagerIdentity(userId));
//
//         }
//
//         public void ClearManager()
//         {
//             var oldUserId = UserIdentityId;
//
//             if (oldUserId == null) throw new FormsInvalidException("NoManager");
//
//             PublishIdentity(new RemoveManagerIdentity(oldUserId.Value));
//             UserIdentityId = null;
//         }
//     }
// }

namespace Domain.Base;