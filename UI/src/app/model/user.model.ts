export interface User {
    id: number;
    firstName: string;
    lastName: string;
    roleId: number;
    roleName: string;
    departmentId: number;
    departmentName: string;
    email: string;
    mobileNo: string;
    password: string;
    address:
    {
        country: string;
        state: string;
    }
}
