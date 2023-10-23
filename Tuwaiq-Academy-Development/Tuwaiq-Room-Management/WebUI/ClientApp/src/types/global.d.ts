
export interface CityDto {
    id: string;
    regionId: string;
    region: RegionDto;
    name: string;
    nameAlt: string | null;
    isActive: boolean;
}

export interface CountryDto {
    id: string;
    name: string;
    nameAlt: string | null;
    isActive: boolean;
    isNational: boolean;
    regions: RegionDto[];
}

export interface EducationLevelDto {
    id: string;
    name: string;
    isActive: boolean;
}

export enum Gander {
    Male = 0,
    Female = 1
}

export interface IdsApplicationDto {
    id: number;
    url: string | null;
    logo: string | null;
    logoAlt: string | null;
    clientId: string | null;
    displayName: string | null;
    displayNames: string | null;
}

export interface IdsContainerDto {
    id: string;
    name: string;
    nameAlt: string | null;
    userId: string;
    idsApplicationId: number;
    idsApplication: IdsApplicationDto;
    expireDate: string | null;
    expireTime: string | null;
}

export interface JobLocationDto {
    id: string;
    name: string;
    isActive: boolean;
}

export interface LanguageDto {
    id: string;
    name: string;
    isActive: boolean;
}

export interface LanguageLevelDto {
    id: string;
    name: string;
    isActive: boolean;
}

export interface LookupDictionary {
    id: string;
    text: string;
}

export interface NationalityDto {
    id: string;
    name: string;
    nameAlt: string | null;
    isNational: boolean;
    isActive: boolean;
}

export interface PaginatedList<T> {
    pageIndex: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}

export interface ProjectTypeDto {
    id: string;
    name: string;
    isActive: boolean;
}

export interface RegionDto {
    id: string;
    countryId: string;
    country: CountryDto;
    name: string;
    nameAlt: string | null;
    isActive: boolean;
    cities: CityDto[];
}

export interface TechnicalDto {
    id: string;
    name: string;
    isActive: boolean;
}

export interface TechnicalLevelDto {
    id: string;
    name: string;
    isActive: boolean;
}

export interface UserIdentityApplicationContainerDto {
    id: string;
    name: string;
    roles: string[];
    selected: boolean;
}

export interface UserIdentityApplicationDto {
    id: number;
    name: string;
    logo: string;
    url: string;
    idsApplicationRoleContainers: UserIdentityApplicationContainerDto[];
}

export interface UserIdentityDto {
    id: string;
    name: string | null;
    userName: string | null;
    email: string | null;
    emailConfirmed: boolean;
    phoneNumber: string | null;
    phoneNumberConfirmed: boolean;
    twoFactorEnabled: boolean;
    lockoutEnd: string | null;
    lockoutEnabled: boolean;
    accessFailedCount: number;
    claims: UserIdentityUserClaimDto[];
    userRoles: UserIdentityUserRoleDto[];
    containers: IdsContainerDto[];
    userIdentitySelectedContainers: UserIdentitySelectedContainerDto[];
    userProfileId: string | null;
    userProfiles: UserProfileDto[];
    roles: (string | null)[];
    roleIds: string[] | null;
    isActive: boolean;
}

export interface UserIdentitySelectedContainerDto {
    userId: string;
    user: UserIdentityDto;
    idsContainerId: string;
    idsContainer: IdsContainerDto;
    idsApplicationId: number;
    idsApplication: IdsApplicationDto;
}

export interface UserIdentityUserClaimDto {
    id: string;
    userId: string;
    claimType: string | null;
    claimValue: string | null;
}

export interface UserIdentityUserRoleDto {
    userId: string;
    roleId: string;
}

export interface UserProfileDto {
    id: string;
    userId: string;
    user: UserIdentityDto;
    firstName: string;
    fatherName: string;
    grandfatherName: string;
    familyName: string;
    firstNameEn: string | null;
    fatherNameEn: string | null;
    grandfatherNameEn: string | null;
    familyNameEn: string | null;
    gander: Gander | null;
    birthdate: string | null;
    nationalityId: string | null;
    nationality: NationalityDto;
    cityId: string | null;
    city: CityDto;
    nationalId: string | null;
    userProfileEducations: UserProfileEducationDto[];
    userProfileExperiences: UserProfileExperienceDto[];
    userProfileLanguages: UserProfileLanguageDto[];
    userProfileTrainings: UserProfileTrainingDto[];
    userProfileProgrammingSkills: UserProfileProgrammingSkillDto[];
    userProfileProjects: UserProfileProjectDto[];
}

export interface UserProfileEducationDto {
    id: string;
    userProfileId: string;
    userProfile: UserProfileDto;
    educationLevelId: string;
    educationLevel: EducationLevelDto;
    countryId: string;
    country: CountryDto;
    educationalEntity: string;
    collage: string;
    speciality: string;
    grade: number;
    gradeOff: number;
    registerYear: number;
    stillStudying: boolean;
    graduationYear: number | null;
}

export interface UserProfileExperienceDto {
    id: string;
    userProfileId: string;
    userProfile: UserProfileDto;
    employer: string;
    jobTitle: string;
    insideKingdom: boolean;
    isGovernance: boolean;
    jobLocationId: string;
    jobLocation: JobLocationDto;
    startDate: string;
    stillWorking: boolean | null;
    endDate: string | null;
    jobDescription: string;
}

export interface UserProfileLanguageDto {
    id: string;
    userProfileId: string;
    userProfile: UserProfileDto;
    languageId: string;
    language: LanguageDto;
    languageLevelId: string;
    languageLevel: LanguageLevelDto;
}

export interface UserProfileProgrammingSkillDto {
    id: string;
    userProfileId: string;
    userProfile: UserProfileDto;
    technicalId: string;
    technical: TechnicalDto;
    years: number;
    technicalLevelId: string;
    technicalLevel: TechnicalLevelDto;
}

export interface UserProfileProjectDto {
    id: string;
    userProfileId: string;
    userProfile: UserProfileDto;
    projectName: string;
    isPrivate: boolean;
    sector: string;
    projectTypeId: string;
    projectType: ProjectTypeDto;
    teamMembersCount: number;
    projectUrl: string | null;
    projectTechnical: string | null;
    projectDescription: string | null;
}

export interface UserProfileTrainingDto {
    id: string;
    userProfileId: string;
    userProfile: UserProfileDto;
    trainer: string;
    startDate: string;
    endDate: string | null;
}

export interface AddUserProfileCommand {
    userId: string;
    firstName: string;
    fatherName: string;
    grandfatherName: string;
    familyName: string;
    firstNameEn: string | null;
    fatherNameEn: string | null;
    grandfatherNameEn: string | null;
    familyNameEn: string | null;
    gander: Gander | null;
    birthdate: string | null;
    nationalityId: string | null;
    cityId: string | null;
    nationalId: string | null;
}

export interface AddUserProfileEducationCommand {
    id: string;
    userId: string;
    userProfileId: string;
    educationLevelId: string;
    countryId: string;
    educationalEntity: string;
    collage: string;
    speciality: string;
    grade: number;
    gradeOff: number;
    registerYear: number;
    stillStudying: boolean;
    graduationYear: number | null;
}

export interface AddUserProfileExperienceCommand {
    id: string;
    userId: string;
    userProfileId: string;
    employer: string;
    jobTitle: string;
    insideKingdom: boolean;
    isGovernance: boolean;
    jobLocationId: string;
    startDate: string;
    stillWorking: boolean | null;
    endDate: string | null;
    jobDescription: string;
}

export interface AddUserProfileLanguageCommand {
    id: string;
    userId: string;
    userProfileId: string;
    languageId: string;
    languageLevelId: string;
}

export interface AddUserProfileProgrammingSkillCommand {
    id: string;
    userId: string;
    userProfileId: string;
    technicalId: string;
    years: number;
    technicalLevelId: string;
}

export interface AddUserProfileProjectCommand {
    id: string;
    userId: string;
    userProfileId: string;
    projectName: string;
    isPrivate: boolean;
    sector: string;
    projectTypeId: string;
    teamMembersCount: number;
    projectUrl: string | null;
    projectTechnical: string | null;
    projectDescription: string | null;
}

export interface AddUserProfileTrainingCommand {
    id: string;
    userId: string;
    userProfileId: string;
    trainer: string;
    startDate: string;
    endDate: string;
}

export interface UpdateUserProfileCommand {
    id: string;
    userId: string;
    firstName: string;
    fatherName: string;
    grandfatherName: string;
    familyName: string;
    firstNameEn: string | null;
    fatherNameEn: string | null;
    grandfatherNameEn: string | null;
    familyNameEn: string | null;
    gander: Gander | null;
    birthdate: string | null;
    nationalityId: string | null;
    cityId: string | null;
    nationalId: string | null;
}

export interface UpdateUserProfileEducationCommand {
    id: string;
    userId: string;
    userProfileId: string;
    educationLevelId: string;
    countryId: string;
    educationalEntity: string;
    collage: string;
    speciality: string;
    grade: number;
    gradeOff: number;
    registerYear: number;
    stillStudying: boolean;
    graduationYear: number | null;
}

export interface UpdateUserProfileExperienceCommand {
    id: string;
    userId: string;
    userProfileId: string;
    employer: string;
    jobTitle: string;
    insideKingdom: boolean;
    isGovernance: boolean;
    jobLocationId: string;
    startDate: string;
    stillWorking: boolean | null;
    endDate: string | null;
    jobDescription: string;
}

export interface UpdateUserProfileLanguageCommand {
    id: string;
    userId: string;
    userProfileId: string;
    languageId: string;
    languageLevelId: string;
}

export interface UpdateUserProfileProgrammingSkillCommand {
    id: string;
    userId: string;
    userProfileId: string;
    technicalId: string;
    years: number;
    technicalLevelId: string;
}

export interface UpdateUserProfileProjectCommand {
    id: string;
    userId: string;
    userProfileId: string;
    projectName: string;
    isPrivate: boolean;
    sector: string;
    projectTypeId: string;
    teamMembersCount: number;
    projectUrl: string | null;
    projectTechnical: string | null;
    projectDescription: string | null;
}

export interface UpdateUserProfileTrainingCommand {
    id: string;
    userId: string;
    userProfileId: string;
    trainer: string;
    startDate: string;
    endDate: string | null;
}