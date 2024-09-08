import { useState } from "react";

export default function UseInput(defaultValue, validationFunction) {
  const [enteredValue, setEnteredValue] = useState(defaultValue);
  const [didTouch, setDidTouch] = useState({
    touch: false,
    classes: "form-control",
    error: "",
  });
  const [error, setError] = useState({
    isError: false,
    errorMessage: "",
  });

  const [didSelect, setDidSelect] = useState({
    select: false,
    classes: "form-select",
    error: "",
  });

  //__________________Check validation onChange__________________

  function handleOnChange(event) {
    setEnteredValue(event.target.value);
    setDidTouch((prev) => {
      return {
        ...prev,
        touch: false,
        classes: "form-control",
        error: "",
      };
    });
    setError((prev) => {
      return {
        ...prev,
        isError: false,
        errorMessage: "",
      };
    });
  }

  //____________________________Check vaidation on blur____________________

  function handleOnBlur() {
    const isDetailsValid = validationFunction(enteredValue);
    if (!isDetailsValid.isValid) {
      setDidTouch((prev) => {
        return {
          ...prev,
          touch: true,
          classes: "form-control is-invalid",
          error: isDetailsValid.errorMessage,
        };
      });
    } else {
      setDidTouch((prev) => {
        return {
          ...prev,
          touch: false,
          classes: "form-control is-valid",
          error: isDetailsValid.errorMessage,
        };
      });
    }
  }

  //_______________________Select input type_____________________

  function handleOnSelect(event) {
    let value = !event ? enteredValue : event.target.value;
    const isOptionValid = validationFunction(value);
    if (!isOptionValid.isValid) {
      setDidSelect((prev) => {
        return {
          ...prev,
          select: true,
          classes: "form-select is-invalid",
          error: isOptionValid.errorMessage,
        };
      });
    } else {
      setDidSelect((prev) => {
        return {
          ...prev,
          select: false,
          classes: "form-select is-valid",
          error: "",
        };
      });
      setEnteredValue(value);
    }
  }

  //__________________function for handling serverside error________________

  function setServerError(errorMessage) {
    setDidTouch((prev) => ({
      ...prev,
      touch: true,
      classes: "form-control is-invalid",
      error: errorMessage,
    }));
  }

  return {
    inputValue: enteredValue,
    handleOnBlur,
    handleOnChange,
    handleOnSelect,
    setServerError,
    error,
    didTouch: didTouch,
    didSelect: didSelect,
  };
}
