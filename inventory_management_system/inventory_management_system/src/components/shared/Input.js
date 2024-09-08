import React from "react";

const Input = ({
  classes,
  htmlFor,
  label,
  id,
  type,
  name,
  placeholder,
  disabled,
  value,
  onChange,
  onBlur,
  error,
}) => {
  return (
    <div className="pb-3">
      <label htmlFor={htmlFor} className="required form-label fw-bold">
        {label}
      </label>
      <input
        id={id}
        type={type}
        name={name}
        value={value}
        disabled={disabled}
        onChange={onChange}
        onBlur={onBlur}
        className={classes}
        placeholder={placeholder}
      />
      {error && <small className="text-danger">{error}</small>}
    </div>
  );
};

export default Input;
