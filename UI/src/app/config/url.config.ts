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
        delete: `user`,
        emailExists: `user/`
    },
    Leave:
    {
        getType: `common/leaveTypes`,
        add: `leave/`,
        get: `leave/user/{userId}`,
        getAll: `leave/department/{departmentId}`,
        updateStatus: `leave/StatusUpdate`,
        report: `leave/report/department/{departmentId}`,
        userReport: `leave/report/user/{userId}`
    },
    Common:
    {
        roles: `common/roles`
    },
    Department:
    {
      get: 'department'
    }

}
