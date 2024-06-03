export interface LoginUser {
    authToken: AuthToken
    role: Role
    rolePrivilege: RolePrivilege[],
    department: Department
    id: number
    passwordHash: string
    passwordSalt: string
    token: any
    firstName: string
    lastName: string
    password: any
    email: string
    roleId: number
  }

  export interface AuthToken {
    userId: number
    email: string
    token: string
    refreshToken: string
  }

  export interface Role {
    roleName: string
    description: string
  }

  export interface RolePrivilege {
    privilegeName: string
    description: string
  }


  export interface Department {
    id: number;
    departmentName: string;
    description: string;
  }
