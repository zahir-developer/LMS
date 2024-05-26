export interface User {
    id: number,
    firstName: string,
    lastName: string,
    roleId: number,
    roleName: string,
    email: string,
    mobileNo: string,
    password: string,
    address:
    {
        country: string,
        state: string
    }
}