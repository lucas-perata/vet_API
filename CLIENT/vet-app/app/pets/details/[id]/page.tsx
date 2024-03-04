import { getPet, getPetMedicalHistory } from "@/app/actions/petActions";
import MedicalHistoryCard from "../MedicalHistoryCard";
import withAuthSSR from "@/utils/withAuthSSR";
import { cookies } from "next/headers";
import React from "react";
import FloatingEditButton from "@/components/ui/FloatingEditButton";
import { LuCake } from "react-icons/lu";
import { MdPets } from "react-icons/md";
import { FaGenderless } from "react-icons/fa";
import { IoIosColorPalette } from "react-icons/io";
import { FaPlus } from "react-icons/fa";
import Link from "next/link";
import { CiCalendarDate } from "react-icons/ci";

async function Details({ params }: { params: { id: number } }) {
  withAuthSSR(cookies().get("token")?.value);
  const data = await getPet(params.id);
  const mH = await getPetMedicalHistory(params.id);
  console.log(data.petPhoto);
  return (
    <div className="flex justify-center gap-5">
      <div className="shadow-2xl flex flex-col gap-5 max-w-2xl rounded-2xl p-2 border ">
        <h1 className="scroll-m-20 text-4xl font-extrabold tracking-tight lg:text-5xl">
          {data.name}
        </h1>

        <div>
          {data.petPhoto.map((photo) => (
            <img
              src={photo.url}
              alt={photo.description}
              key={photo.id}
              className="rounded-2xl"
            />
          ))}
        </div>
        <div className="flex items-center gap-5">
          <MdPets size={30} />
          {data.breed}
        </div>
        <div className="flex items-center gap-5">
          <CiCalendarDate size={30} /> {data.dateOfBirth}
        </div>
        <div className="flex items-center gap-5">
          <LuCake size={30} /> {data.dateOfBirth}
        </div>
        <div className="flex items-center gap-5">
          <FaGenderless size={30} /> {data.gender}
        </div>
        <div className="flex items-center gap-5">
          <IoIosColorPalette size={30} /> {data.color}
        </div>
        {data.isNeutered == true ? "Castrado" : null}
      </div>
      <div className="flex justify-center gap-5 flex-wrap mt-10">
        <div className="flex flex-col gap-5 ">
          <h2 className="scroll-m-20 pb-2 text-3xl font-semibold tracking-tight first:mt-0 self-center flex items-center gap-4">
            Historial médico
            <Link href="">
              <FaPlus size={25} />
            </Link>
          </h2>
          {mH.map((mh) => (
            <MedicalHistoryCard medicalHistory={mh} key={mh.id} />
          ))}
        </div>
        <div className="flex flex-col gap-5 ">
          <h2 className="scroll-m-20 pb-2 text-3xl font-semibold tracking-tight first:mt-0 self-center flex items-center gap-4">
            Vacunas
            <Link href="">
              <FaPlus size={25} />
            </Link>
          </h2>
          {mH.map((mh) => (
            <MedicalHistoryCard medicalHistory={mh} key={mh.id} />
          ))}
        </div>
        <div className="flex flex-col gap-5 ">
          <h2 className="scroll-m-20 pb-2 text-3xl font-semibold tracking-tight first:mt-0 self-center flex items-center gap-4">
            Gastos asociados
            <Link href="">
              <FaPlus size={25} />
            </Link>
          </h2>
          {mH.map((mh) => (
            <MedicalHistoryCard medicalHistory={mh} key={mh.id} />
          ))}
        </div>
      </div>
      <FloatingEditButton route={`/pets/update/${data.id}`} />
    </div>
  );
}

export default Details;
