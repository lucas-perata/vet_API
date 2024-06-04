"use client"
import React from "react";
import { getRole } from "../actions/getRole";
import useStore from "@/store/store";
import NavbarFront from "./NavbarFront";
import NavbarApp from "./NavbarApp";

export default function Navbar() {

  const token = useStore((state) => state.token);
  const kookies = token();
  let role: string;
  let front: boolean = true;

  if (kookies) {
    role = getRole(kookies);
    front = false;
  }


  // TODO: diferent navbar for front end and APP side

  return (
    <>
      {front ? <NavbarFront kookies={kookies} role={role} /> : <NavbarApp kookies={kookies} role={role} />}
    </>
  )
}
