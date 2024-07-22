import { createInstance } from '@/utils/axiosConfig';
import { cookies } from 'next/headers'
import React from 'react'
import { redirect } from "next/navigation";

function page() {
  const token: string | undefined = cookies().get("token")?.value;
  if (token == undefined) redirect("/app/sessions/login");
  const instance = createInstance(token);
  return (
    <div>page</div>
  )
}

export default page;
