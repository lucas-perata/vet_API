"use client"
import React from "react";
import { VetMain } from "./vetMain";
import { OwnerMain } from "./ownerMain";
import useStore from "@/store/store";
import { getRole } from "../actions/getRole";

export default function page() {
  const token = useStore((state) => state.token())
  const role = getRole(token);

  return <div>
    {role == "Vet" ? <VetMain /> : <OwnerMain />}

  </div>
}
