import React from "react";
import { UseControllerProps } from "react-hook-form";
import { FormControl, FormField, FormItem, FormMessage } from "../ui/form";
import { Input } from "../ui/input";

type Props = {
  label: string;
  type?: string;
  name: string;
  form: any;
} & UseControllerProps;

function InputForm(props: Props) {
  return (
    <div className="mb-3 block">
      <FormField
        control={props.form.control}
        name={props.name}
        render={({ field }) => (
          <FormItem>
            <FormControl>
              <Input
                type={props.type || "text"}
                {...field}
                placeholder={props.label}
                value={field.value || ""}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
    </div>
  );
}

export default InputForm;
