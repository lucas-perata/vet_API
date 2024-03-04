import React from "react";
import { Button } from "./button";
import { FaPlus } from "react-icons/fa";
import Link from "next/link";

type Props = {
  route: string;
  label?: string;
};

function FloatingButton(props: Props) {
  return (
    <div className="fixed bottom-4 right-4">
      <Link href={props.route}>
        <Button variant="ghost">
          <FaPlus size={40} color="green" />
        </Button>
      </Link>
    </div>
  );
}

export default FloatingButton;
