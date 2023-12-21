.globl stosd

.intel_syntax noprefix
stosd:
	mov rdi, rdi
	mov rcx, rsi
	mov rax, rdx
	rep stosd
	ret
