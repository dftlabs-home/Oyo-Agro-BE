# Password Reset and Forgot Password Implementation

This document describes the comprehensive password reset and forgot password functionality implemented for the OyoAgro system.

## Overview

The implementation follows the system's existing patterns and provides a secure, efficient, and fast password reset mechanism with the following features:

- **Secure token generation** using cryptographic random number generation
- **Email-based password reset** with professional HTML templates
- **Token expiration** (24 hours) for security
- **Token invalidation** after use or when new tokens are generated
- **Comprehensive validation** and error handling
- **Database tracking** of reset attempts and usage

## Architecture

### Entities

#### PasswordResetToken
- Stores reset tokens with expiration and usage tracking
- Links to Useraccount via foreign key
- Includes IP address and user agent tracking for security

#### Useraccount (Updated)
- Added password reset fields:
  - `PasswordResetToken`: Current active token
  - `PasswordResetTokenExpires`: Token expiration time
  - `LastPasswordReset`: Timestamp of last password reset

### Services

#### PasswordResetService
- `ForgotPassword()`: Initiates password reset process
- `ResetPassword()`: Resets password using valid token
- `ValidateResetToken()`: Validates token without resetting password

#### UserService (Updated)
- Added password reset methods that delegate to PasswordResetService
- Maintains consistency with existing service patterns

### Repositories

#### PasswordResetTokenRepository
- `GetValidToken()`: Retrieves valid, unused, non-expired tokens
- `GetUserTokens()`: Gets all tokens for a user
- `SaveForm()`: Creates new tokens
- `UpdateToken()`: Updates token status
- `DeleteExpiredTokens()`: Cleanup expired tokens

### Controllers

#### UserController (Updated)
- `POST /api/v1.0/user/forgot-password`: Initiate password reset
- `POST /api/v1.0/user/reset-password`: Reset password with token
- `GET /api/v1.0/user/validate-reset-token`: Validate token

## API Endpoints

### 1. Forgot Password
```http
POST /api/v1.0/user/forgot-password
Content-Type: application/json

{
  "email": "user@example.com"
}
```

**Response:**
```json
{
  "success": true,
  "Data": {
    "Tag": 1,
    "Message": "If the email address exists in our system, you will receive a password reset link.",
    "Data": null
  }
}
```

### 2. Reset Password
```http
POST /api/v1.0/user/reset-password
Content-Type: application/json

{
  "token": "reset-token-here",
  "newPassword": "newPassword123",
  "confirmPassword": "newPassword123"
}
```

**Response:**
```json
{
  "success": true,
  "Data": {
    "Tag": 1,
    "Message": "Password has been reset successfully. You can now login with your new password.",
    "Data": null
  }
}
```

### 3. Validate Reset Token
```http
GET /api/v1.0/user/validate-reset-token?token=reset-token-here
```

**Response:**
```json
{
  "success": true,
  "Data": {
    "Tag": 1,
    "Message": "Token is valid.",
    "Data": true
  }
}
```

## Security Features

### Token Security
- **Cryptographically secure tokens**: Generated using `RandomNumberGenerator`
- **URL-safe encoding**: Base64 with URL-safe characters
- **64-byte length**: Sufficient entropy for security
- **Single use**: Tokens are invalidated after use

### Email Security
- **No user enumeration**: Same response whether email exists or not
- **Secure email templates**: Professional HTML with security warnings
- **Token expiration**: 24-hour limit on token validity

### Database Security
- **Foreign key constraints**: Ensures data integrity
- **Indexed queries**: Fast token lookups
- **Cascade deletion**: Cleanup when users are deleted

## Email Templates

### Password Reset Email
- Professional HTML template with company branding
- Clear call-to-action button
- Security warnings and instructions
- Fallback text template if HTML template is missing
- Responsive design for mobile devices

## Configuration

### Environment Variables
- `RESEND_API_KEY`: Required for sending emails
- `API_BASE_URL`: Base URL for reset links (update in GlobalConstant.cs)

### Database Migration
The implementation requires a database migration to add the `passwordresettokens` table and update the `useraccount` table.

## Usage Examples

### Frontend Integration

#### Forgot Password Form
```javascript
async function forgotPassword(email) {
  const response = await fetch('/api/v1.0/user/forgot-password', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ email })
  });
  
  const result = await response.json();
  if (result.success) {
    alert('Check your email for password reset instructions');
  } else {
    alert('Error: ' + result.Data.Message);
  }
}
```

#### Reset Password Form
```javascript
async function resetPassword(token, newPassword, confirmPassword) {
  const response = await fetch('/api/v1.0/user/reset-password', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      token,
      newPassword,
      confirmPassword
    })
  });
  
  const result = await response.json();
  if (result.success) {
    alert('Password reset successfully! You can now login.');
    window.location.href = '/login';
  } else {
    alert('Error: ' + result.Data.Message);
  }
}
```

## Error Handling

The implementation includes comprehensive error handling:

- **Input validation**: Email format, password strength, token presence
- **Business logic validation**: User status, token validity, expiration
- **Database errors**: Graceful handling of database operations
- **Email errors**: Fallback handling for email delivery failures
- **Security errors**: Proper error messages without information leakage

## Performance Considerations

- **Efficient queries**: Indexed database lookups
- **Token cleanup**: Automatic cleanup of expired tokens
- **Async operations**: Non-blocking database and email operations
- **Minimal data transfer**: Only necessary data in API responses

## Testing

### Unit Tests
- Test token generation and validation
- Test email sending functionality
- Test password reset flow
- Test error handling scenarios

### Integration Tests
- Test complete forgot password flow
- Test complete reset password flow
- Test token expiration handling
- Test concurrent reset requests

## Maintenance

### Regular Cleanup
- Implement a scheduled job to clean up expired tokens
- Monitor email delivery success rates
- Review security logs for suspicious activity

### Monitoring
- Track password reset request frequency
- Monitor token usage patterns
- Alert on unusual reset patterns

## Security Recommendations

1. **Rate limiting**: Implement rate limiting on forgot password endpoints
2. **IP tracking**: Log IP addresses for security monitoring
3. **Audit logging**: Log all password reset attempts
4. **Email verification**: Consider requiring email verification before reset
5. **Password history**: Prevent reuse of recent passwords

## Conclusion

This implementation provides a robust, secure, and user-friendly password reset system that follows the existing OyoAgro system patterns. It includes comprehensive security measures, professional email templates, and efficient database operations while maintaining the system's architectural consistency.
