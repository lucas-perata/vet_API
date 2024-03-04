import React from "react";
import { Button } from "./button";
import Link from "next/link";
import { FaEdit } from "react-icons/fa";

type Props = {
  route: string;
  label?: string;
};

function FloatingEditButton(props: Props) {
  return (
    <div className="fixed bottom-4 right-4">
      <Link href={props.route}>
        <Button variant="ghost">
          <FaEdit size={40} color="green" />
        </Button>
      </Link>
    </div>
  );
}

export default FloatingEditButton;
