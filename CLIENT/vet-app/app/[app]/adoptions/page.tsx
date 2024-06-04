"use client";
import React from "react";
import AdoptionsList from "./AdoptionsList";
import SearchAdoptions from "./SearchAdoptions";
import withAuth from "@/utils/withAuth";
import FloatingButton from "@/components/ui/FloatingButton";

function page() {
  return (
    <div className="flex flex-col items-center">
      <SearchAdoptions />
      <AdoptionsList />
      <FloatingButton route="/adoptions/add" />
    </div>
  );
}

export default page;
