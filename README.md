# LMS

#References:
Udemy course:
https://www.udemy.com/course/build-an-app-with-aspnet-core-and-angular-from-scratch/learn/lecture/22401746#overview

Clean architecture:
1# https://medium.com/@codebob75/repository-pattern-c-ultimate-guide-entity-framework-core-clean-architecture-dtos-dependency-6a8d8b444dcb#:~:text=Repository%20Pattern%20introduction,-Without%20Repository%20pattern&text=In%20a%20given%20project%2C%20one,a%20repository%20for%20every%20DbSet

JWT policy Authorization:
https://medium.com/@bruno-bernardes-tech/how-to-implement-jwt-authentication-in-asp-net-core-269f258f19be

https://github.com/Brunosalesb/jwt-auth/blob/master/jwtAuth/Program.cs#L52

https://medium.com/@bruno-bernardes-tech/how-to-implement-jwt-authentication-in-asp-net-core-269f258f19be#:~:text=To%20use%20JWT%2C%20we%20need,this%20package%20to%20the%20project.&text=Now%2C%20create%20the%20AuthService%20(commonly,and%20returns%20the%20generated%20token

EF No Triacking 
https://learn.microsoft.com/en-us/ef/core/querying/tracking
/*
optionsBuilder
        .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFQuerying.Tracking;Trusted_Connection=True;ConnectRetryCount=0")
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

*/

Bootstrap Icons:

/*
npm i bootstrap-icons --save

@import "~bootstrap-icons/font/bootstrap-icons.css";

<i class="bi bi-star-fill"></i>
*/