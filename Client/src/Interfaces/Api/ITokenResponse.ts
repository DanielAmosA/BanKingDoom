// The interface responsible for Structure declaration for TokenResponse
export interface ITokenResponse {
    success: boolean;
    token: string;
    expiresAt: string;
}