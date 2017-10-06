USE CVGS
GO

declare @MemberId INT;

EXECUTE SP_MEMBER_LOGIN
	@UserName = 'doug.epp',
	@pwd='Initial',
	@MemberId = @MemberId OUTPUT
SELECT @MemberId