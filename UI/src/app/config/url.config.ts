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
        getType: `common/leaveTypes`,
        add: `leave/`,
        get: `leave/`, 
        getAll: `leave/`,
        updateStatus: `leave/StatusUpdate`
    }
}