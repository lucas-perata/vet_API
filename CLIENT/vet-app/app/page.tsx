"use client"
import useStore from "@/store/store";
import { getRole } from "./actions/getRole";
import { VetMain } from "./dashboard/vetMain";
import { OwnerMain } from "./dashboard/ownerMain";

export default function Home() {
  const token = useStore((state) => state.token());
  const role = getRole(token);
  return (
    <div>
      {
        role == "Vet" ? <VetMain /> : <OwnerMain />
      }
    </div>
  );
}
