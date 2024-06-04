"use client"
import React from "react";
import { VetMain } from "./vetMain";
import { OwnerMain } from "./ownerMain";
import useStore from "@/store/store";
import { getRole } from "@/app/actions/getRole";
import withAuth from "@/utils/withAuth";

function page() {
  const token = useStore((state) => state.token())
  const role = getRole(token);

  return <div>
    {role == "Vet" ? <VetMain /> : <OwnerMain />}

  </div>
}

export default withAuth(page);
