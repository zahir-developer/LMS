export interface User {
    firstName: string,
    lastName: string,
    emailId: string,
    mobileNo: string,
    password: string,
    address:
    {
        country: string,
        state: string
    }
}