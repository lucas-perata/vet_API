import { jwtDecode } from "jwt-decode";

export function getRole(token: string | undefined) {
  if (token == undefined) return;
  let decodedToken = jwtDecode(token);
  let role = decodedToken.role;
  return role;
}
