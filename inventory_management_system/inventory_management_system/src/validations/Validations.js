export function validateUserName(inputValue) {
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Name is required!",
    };
  } else if (inputValue.trim().length > 30) {
    return {
      isValid: false,
      errorMessage: "Name cannot exceed 30 characters.",
    };
  } else if (!/^[A-Za-z]+$/.test(inputValue) || /\s/.test(inputValue)) {
    return {
      isValid: false,
      errorMessage: "Name can only contain letters but spaces not allowed.",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

export function validateEmail(inputValue) {
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Email is required!",
    };
  } else if (inputValue.trim().length > 100) {
    return {
      isValid: false,
      errorMessage: "Email cannot exceed 100 characters.",
    };
  } else if (
    /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}(?:\.[a-zA-Z]{2,})?$/u.test(
      inputValue
    )
  ) {
    return {
      isValid: true,
      errorMessage: "",
    };
  } else {
    return {
      isValid: false,
      errorMessage: "Email must be a valid email address!",
    };
  }
}

export function validatePassword(passwordValue) {
  if (passwordValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Password is required!",
    };
  } else if (
    !/^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{6,20}$/.test(
      passwordValue
    )
  ) {
    return {
      isValid: false,
      errorMessage:
        "Password must be a valid password containing min 6 and maximum 20 letter password, with at least a symbol, upper and lower case letters and a number",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

export function validateConfirmPassword(confirmPassword, password) {
  if (confirmPassword.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Confirm password is required!",
    };
  } else if (
    !/^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{6,20}$/.test(
      confirmPassword
    )
  ) {
    return {
      isValid: false,
      errorMessage:
        "Confirm password must be a valid password containing min 8 and maximum 20 letter password, with at least a symbol, upper and lower case letters and a number",
    };
  } else if (password !== confirmPassword) {
    return {
      isValid: false,
      errorMessage: "password didn't match!",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

export function validatePhoneNo(inputValue) {
  if (typeof inputValue === "number") inputValue = inputValue + "";
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Phone number is required!",
    };
  } else if (inputValue.trim().length > 10) {
    return {
      isValid: false,
      errorMessage: "Phone number cannot exceed 10 characters.",
    };
  } else if (!/^\d{10}$/.test(inputValue)) {
    return {
      isValid: false,
      errorMessage: "Phone Number must be a valid 10-digit number",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

export function validateProductName(inputValue) {
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Product name is required!",
    };
  } else if (inputValue.length > 50) {
    return {
      isValid: false,
      errorMessage: "Product name contains maximum 50 characters.",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

function isValidFormatAsNumber(inputValue) {
  for (let i = 0; i < inputValue.length; i++) {
    if (!(inputValue[i] >= 0 && inputValue[i] <= 9)) {
      return false;
    }
  }
  return true;
}

export function validateQuantity(inputValue) {
  if (typeof inputValue === "number") {
    inputValue = inputValue + "";
  }
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Quantity is required!",
    };
  } else if (inputValue < 0) {
    return {
      isValid: false,
      errorMessage: "Quantity must be positive.",
    };
  } else if (!isValidFormatAsNumber(inputValue)) {
    return {
      isValid: false,
      errorMessage: "Please only enter numeric characters.",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}
export function validateInventoryRequestQuantity(inputValue) {
  if (typeof inputValue === "number") {
    inputValue = inputValue + "";
  }
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Quantity is required!",
    };
  } else if (inputValue <= 0) {
    return {
      isValid: false,
      errorMessage: "Quantity must be greater than 0.",
    };
  } else if (!inputValue.match(/^\d+/)) {
    return {
      isValid: false,
      errorMessage: "Please only enter numeric characters.",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

export function validatePrice(inputValue) {
  if (typeof inputValue === "number") {
    inputValue = inputValue + "";
  }
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Price is required!",
    };
  } else if (inputValue <= 0) {
    return {
      isValid: false,
      errorMessage: "Price must be greater than 0.",
    };
  } else if (!/^[1-9]\d*(\.\d+)?$/.test(inputValue)) {
    return {
      isValid: false,
      errorMessage: "Price must be a valid digit",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

export function validateCategory(inputValue) {
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Category is required!",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}

export function validateReason(inputValue) {
  if (inputValue.trim() === "") {
    return {
      isValid: false,
      errorMessage: "Reason is required!",
    };
  } else {
    return {
      isValid: true,
      errorMessage: "",
    };
  }
}
