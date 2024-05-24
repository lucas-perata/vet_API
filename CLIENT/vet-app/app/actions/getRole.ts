import { jwtDecode } from "jwt-decode";

export function getRole(token: string) {
  let decodedToken = jwtDecode(token);
  let role = decodedToken.role;
  return role;
}
