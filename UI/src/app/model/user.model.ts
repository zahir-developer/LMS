export class user
{
    constructor(public firstName: string,
         public lastName: string,
         public emailId: string,
         public mobileNo: string,
         public password: string,
         public address:
         {
            country: string,
            state: string
         })
    {

    }
}