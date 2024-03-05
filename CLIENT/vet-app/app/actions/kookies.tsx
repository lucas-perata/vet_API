import { cookies } from "next/headers";

export const kookies = cookies().get("token")?.value;
