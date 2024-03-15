"use client";
import React from "react";
import AdoptionsList from "./AdoptionsList";
import SearchAdoptions from "./SearchAdoptions";
import withAuth from "@/utils/withAuth";

function page() {
  return (
    <div className="flex flex-col items-center">
      <SearchAdoptions />
      <AdoptionsList />
    </div>
  );
}

export default withAuth(page);
