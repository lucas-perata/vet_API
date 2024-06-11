import { getRole } from '@/app/actions/getRole';
import React from 'react'
import { OwnerMain } from './ownerMain';
import { cookies } from 'next/headers';
import { redirect } from "next/navigation";
import { VetMain } from './vetMain';

function page() {
  // const token: string = useStore((state) => state.token());
  const token: string | undefined = cookies().get("token")?.value;
  if (token == undefined) redirect("/app/sessions/login");
  const role: string | undefined = getRole(token);

  if (role == "Vet") {
    return <div className="h-[85vh] flex justify-center"><VetMain token={token} /></div>
  } else {
    return <OwnerMain token={token} />
  }
}

export default page;

