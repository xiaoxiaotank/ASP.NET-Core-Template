export class Account{
    user: User;
    jwt: JwtResponse;
}

class User{
    id: number;
    userName: string;
    email: string;
}

class JwtResponse{
    status: boolean;
    accessToken: string;
    expiresIn: number;
    tokenType: string;
}