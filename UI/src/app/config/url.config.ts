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
        getAll: `user/users`,
        update: `user/`,
        delete: `user`
    },
    Leave:
    {
        getType: `common/leaveTypes`,
        add: `leave/`,
        get: `leave/`, 
        getAll: `leave/`,
        updateStatus: `leave/StatusUpdate`
    },
    Common:
    {
        roles: `common/roles`
    }

}