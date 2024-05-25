export const apiEndPoint = 
{
    Auth:
    {
        //signup: `auth/AddAdmin`,
        signup: `auth/signup`,
        login: `auth/login`
    },
    User:
    {
        getAll: `user/users`
    },
    Leave:
    {
        add: `leave/`,
        get: `leave/{userId}`,
        getAll: `leave/`,
        getType: `common/leaveTypes`
    }
}