import { jwtDecode } from "jwt-decode";

export function getRole(token: string) {
  if (token == null) return;
  let decodedToken = jwtDecode(token);
  let role = decodedToken.role;
  return role;
}
