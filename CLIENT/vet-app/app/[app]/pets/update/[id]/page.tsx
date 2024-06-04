import React from "react";

export default function UpdatePet({ params }: { params: { id: number } }) {
  return <div>Update {params.id}</div>;
}
